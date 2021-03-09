X=rand(1000);
% min je parametr a=0; maximum je parametr b=1
%rovnomìrné rozdìlení má støední hodnotu 0.5
%rovnomìrné rozdìlení má rozptyl (a-b)^2/12, tj. 1/12

%centrální limitní vìta k prùmìru
mu=0.5;
sigma2=(1/12)/1000;
sigma=sqrt(sigma2)

%vykreslení normálního rozdìlení s parametry (mu,sigma2)
x=0.45:0.001:0.55;
y=normpdf(x,mu,sigma);
plot(x,y);

%výpoèet pravdìpodobnosti pøes z-skore
%testujeme, že transformace na normované normální rozdìlení funguje
z=(0.52-mu)/sigma;
Pravd=1-normcdf(z,0,1)

%výpoèet pravdìpodobnosti pøímo
Pravd=1-normcdf(0.52,mu,sigma)
