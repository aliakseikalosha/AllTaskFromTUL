%každý pokus má støední hodnotu 3 chyby a rozptyl 4 chyby na druhou
str_hodnota=(1+2+3+4+5+6)/6;
rozptyl=2*((1-str_hodnota)^2+(2-str_hodnota)^2+(3-str_hodnota)^2)/6;

% centrální limitní vìta
pocet_hodu=100;
mu=str_hodnota*pocet_hodu;
sigma2=rozptyl*pocet_hodu;
sigma=sqrt(sigma2);

P=normcdf(380.5,mu,sigma)-normcdf(319.5,mu,sigma)