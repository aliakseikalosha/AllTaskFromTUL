lambda=sym('lambda');
t=sym('t');

%výpoèet distribuèní funkce
y=lambda*exp(-(lambda*t));
z=int(y)

%výpoèet limity
limit(z,0)