%exponenciální rozdìlení má støední hodnotu 1/lambda a to je 5 let
%rozptyl je shodný jako støední hodnota, tj. 1/lambda, tj. 5 let 
%je to vlastnost exponenciálního rozdìlení

exp_stredni_hodnota=5;
%získáme parametry normálního rozdìlení
mu=(100*exp_stredni_hodnota)/100;
sigma2=(exp_stredni_hodnota^2)/100;
sigma=sqrt(sigma2);

%získáme pravdìpodobnost pøes zskore
z=(4-mu)/sigma;
Pravd=normcdf(z,0,1)

%získání pravdìpodobnosti pøímo
z=normcdf(4,mu,sigma)

%vykreslení normálního rozdìlení s parametry (mu,sigma2)
x=3.5:0.01:6.5;
y=normpdf(x,mu,sigma);
plot(x,y);


