using System.Text;


namespace Arca.BarcInterpreter
{
	public struct BarcConstant
	{
		public readonly BarcConstantType type;
		public readonly int position;

		public BarcConstant(BarcConstantType type, int position)
		{
			this.type = type;
			this.position = position;
		}

		public string GetString(BarcReader reader)
		{
			int prevPosition = reader.SetPosition(position);
			string value = reader.GetString();
			reader.SetPosition(prevPosition);
			return value;
		}

		public int GetInt(BarcReader reader)
		{
			int prevPosition = reader.SetPosition(position);
			int value = reader.GetInt();
			reader.SetPosition(prevPosition);
			return value;
		}

		public byte GetByte(BarcReader reader)
		{
			int prevPosition = reader.SetPosition(position);
			byte value = reader.GetByte();
			reader.SetPosition(prevPosition);
			return value;
		}
	}

	public enum BarcConstantType : byte
	{
		String,
		Int,
		Byte,
	}
}