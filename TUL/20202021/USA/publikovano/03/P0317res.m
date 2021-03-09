x=unifrnd(0,1,1000,1);
x=x.^0.5;
x=fix(6.*x+1);

median(x)
mode(x)
hist(x)