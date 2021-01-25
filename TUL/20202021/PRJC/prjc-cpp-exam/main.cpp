#include <iostream>

int main() {
    int* a = (int*)malloc(sizeof(int));
    a = new int[2];
    *a = 10;
    std::cout << "Hello, World! address :"<< a <<" : "<< *a << std::endl;
    delete a;
    return 0;
}
