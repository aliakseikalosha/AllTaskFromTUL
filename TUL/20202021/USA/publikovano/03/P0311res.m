lambda=sym('lambda');
t=sym('t');

%v�po�et distribu�n� funkce
y=lambda*exp(-(lambda*t));
z=int(y)

%v�po�et limity
limit(z,0)