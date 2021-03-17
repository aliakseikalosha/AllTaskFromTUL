clear all
clc

syms lambda x positive
a=simplify(solve(1-exp(-lambda*60) == 0.3))

EX=1/a;