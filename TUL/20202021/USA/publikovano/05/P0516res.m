syms lambda
t=3/lambda
%pravdìpodobnost poruchy
Fx=1-exp(-lambda*t)

%pravdìpodobnost bez poruchy, jednotkový doplnìk
Rx=exp(-lambda*t)