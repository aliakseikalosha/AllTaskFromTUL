%norm�ln� rozd�len� s parametry N(0,3)

pravd_a=normcdf(5,0,3)-normcdf(-2,0,3)

%pravd�podobnost, �e 0 v�robk� bude m�t chybu mimo interval
%pravd�poodbnost mimo interval je 1-prav_a
pravd_b=1-pravd_a;
%binomick� rozd�len�, v�echny budou m�t chybu mimo interval
pravd_b=binopdf(0,3,pravd_b);
%alespo� jeden bude m�t chybu mimo interval
pravd_b=1-pravd_b