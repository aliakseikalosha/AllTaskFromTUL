%každý pokus má støední hodnotu 90 kg a smìrodatnou odchylku 10 kg
str_hodnota=90;
sm_odchylka=10;
rozptyl=sm_odchylka^2;

% centrální limitní vìta
pocet_mist=64;
mu=str_hodnota*pocet_mist;
sigma2=rozptyl*pocet_mist;
sigma=sqrt(sigma2);
z=(6000-mu)/sigma;

P=1-normcdf(6000,mu,sigma)
