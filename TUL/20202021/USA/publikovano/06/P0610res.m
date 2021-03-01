%každý pokus má støední hodnotu 3 chyby a rozptyl 4 chyby na druhou
str_hodnota=3;
rozptyl=4;

% centrální limitní vìta
pocet_stran=400;
mu=str_hodnota*pocet_stran;
sigma2=rozptyl*pocet_stran;
sigma=sqrt(sigma2);

P=normcdf(1000,mu,sigma)