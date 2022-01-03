#include <iostream>
#include <fstream>

using namespace std;

std::ifstream::pos_type filesize(const char* filename)
{
    ifstream in(filename, ifstream::ate | ifstream::binary);
    return in.tellg();
}

int main()
{
    cout << "Hello World!\n";
    ifstream file;
    const char* path = "E:\\Projects\\Visual Studio\\Arca\\ArcaStandard\\test.barc";
    file.open(path);
    if (!file) return 0;

    uint64_t remaining = filesize(path);
    int data = file.get();
    while (remaining > 0)
    {
        cout << data << "\n";
        data = file.get();
        remaining--;
    }
    file.close();
    return 0;
}