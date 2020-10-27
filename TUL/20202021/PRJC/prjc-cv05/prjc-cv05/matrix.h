#pragma once


int** create(int n, int m);
int** add(int** a,int** b,int n, int m);
int** multiply(int** a, int** b, int n, int m);
void print(int** matrix, int n, int m);
void fill(int** matrix, int n, int m);
void freeMemory(int** matrix, int n);