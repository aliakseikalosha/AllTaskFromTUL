%normální rozdìlení s parametry N(0,3)

pravd_a=normcdf(5,0,3)-normcdf(-2,0,3)

%pravdìpodobnost, že 0 výrobkù bude mít chybu mimo interval
%pravdìpoodbnost mimo interval je 1-prav_a
pravd_b=1-pravd_a;
%binomické rozdìlení, všechny budou mít chybu mimo interval
pravd_b=binopdf(0,3,pravd_b);
%alespoò jeden bude mít chybu mimo interval
pravd_b=1-pravd_b