% plat N(mu=27000, sd=8000)
% bydleni N(mu=7000,sd=2000)

mu_plat=27000;
sd_plat=8000;

mu_bydleni=7000;
sd_bydleni=2000;

mu=mu_plat-mu_bydleni;
sd=sqrt(sd_plat^2+sd_bydleni^2);
pravd=1-normcdf(25000,mu,sd)