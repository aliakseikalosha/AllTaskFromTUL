clear all
clc

syms lambda positive 
t=3/lambda
%pravděpodobnost poruchy
Fx=1-exp(-lambda*t)

%pravděpodobnost bez poruchy, jednotkový doplněk
Rx=exp(-lambda*t)