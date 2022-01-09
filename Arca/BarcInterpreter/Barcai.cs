using System.Diagnostics;
using System;
using System.Collections.Generic;

namespace Arca.BarcInterpreter
{
	public class Barcai
	{
		private readonly BarcReader reader = new();
		private readonly BarcConstant[] constantTable;
		private readonly BarcValue[] variableTable = new BarcValue[256];
		private readonly Stack<BarcValue> stack = new();
		private readonly BarcReference[] heap = new BarcReference[256];


		public Barcai(byte[] bytecode)
		{
			reader.SetSource(bytecode);
			int constantTableSize = reader.GetByte();
			constantTable = new BarcConstant[constantTableSize];
			for (int i = 0; i < constantTableSize; i++)
			{
				BarcConstantType type = (BarcConstantType)reader.GetByte();
				int position = reader.Position;
				constantTable[i] = new BarcConstant(type, position);
				switch (type)
				{
					case BarcConstantType.String:
						reader.SkipBytes(reader.GetInt());
						break;
					case BarcConstantType.Int:
						reader.SkipBytes(4);
						break;
					case BarcConstantType.Byte:
						reader.SkipBytes(1);
						break;
				}
			}
		}

		public void Interpret()
		{
			Stopwatch sw = Stopwatch.StartNew();
			bool error = false;
			BarcOpcode errorOpcode = BarcOpcode.Noop;
			while (!reader.EndOfData && !error)
			{
				BarcOpcode opcode = (BarcOpcode)reader.GetByte();
				switch (opcode)
				{
					case BarcOpcode.Noop:
						break;
					case BarcOpcode.Push:
						Push();
						break;
					case BarcOpcode.Print:
						Print();
						break;
					case BarcOpcode.DeclareConstant:
						DeclareConstant();
						break;
					case BarcOpcode.Release:
						Release();
						break;
					case BarcOpcode.JumpRelativeSByte:
						reader.SetPosition(reader.Position + reader.GetSByte());
						break;
					case BarcOpcode.JumpRelativeShort:
						reader.SetPosition(reader.Position + reader.GetShort());
						break;
					case BarcOpcode.JumpAbsoluteUShort:
						reader.SetPosition(reader.GetUShort());
						break;
					case BarcOpcode.JumpAbsoluteInt:
						reader.SetPosition(reader.GetInt());
						break;
					default:
						error = true;
						errorOpcode = opcode;
						break;
				}
			}
			sw.Stop();
			if (error)
				Console.WriteLine($"Runtime Error:\n\tBefore Byte: {reader.Position}\n\tWhilst Executing: {errorOpcode}");
			//Console.WriteLine((sw.ElapsedTicks / 10000d) + "ms");
			Console.WriteLine((sw.ElapsedTicks / 1000000d) + "ms");
		}

		private void Push()
		{
			byte position = reader.GetByte();
			stack.Push(variableTable[position]);
		}

		private void Print()
		{
			BarcValue value = stack.Pop();
			switch (value.type)
			{
				case BarcValueType.Reference:
					BarcReference reference = heap[value.Abyte];
					Console.WriteLine(reference.data);
					break;
				case BarcValueType.Int:
					Console.WriteLine(value.Aint);
					break;
				case BarcValueType.Byte:
					Console.WriteLine(value.Abyte);
					break;
			}
		}

		private void DeclareConstant()
		{
			byte position = reader.GetByte();
			BarcConstant constant = constantTable[reader.GetByte()];
			BarcValue value = new();
			switch (constant.type)
			{
				case BarcConstantType.String:
					value.type = BarcValueType.Reference;
					value.Abyte = position;
					StringData data = new(constant.GetString(reader));
					heap[position] = new BarcReference(BarcReferenceType.String, data);
					break;
				case BarcConstantType.Int:
					value.type = BarcValueType.Int;
					value.Aint = constant.GetInt(reader);
					break;
				case BarcConstantType.Byte:
					value.type = BarcValueType.Byte;
					value.Abyte = constant.GetByte(reader);
					break;
			}
			variableTable[position] = value;
		}

		private void Release()
		{
			byte position = reader.GetByte();
			BarcValue value = variableTable[position];
			variableTable[position].type = BarcValueType.Invalid;
			if (value.type == BarcValueType.Reference)
			{
				heap[value.Abyte] = null;
			}
		}
	}
}