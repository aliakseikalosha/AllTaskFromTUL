clear all
clc

% a)
m=10
n=6
Kombinace_faktorial=factorial(m)/(factorial(n)*factorial(m-n))

Kombinace_funkce=nchoosek(m,n)

% b)
m=100
n=60
kombinace_b=factorial(m)/(factorial(n)*factorial(m-n))
kombinace_funkce_b=nchoosek(m,n)

% c)
m=1000
n=600
kombinace_c=factorial(m)/(factorial(n)*factorial(m-n))
kombinace_funkce_c=nchoosek(m,n)
