x10=normrnd(0,1,1,10);
x100=normrnd(0,1,1,100);
x1000=normrnd(0,1,1,1000);
x10000=normrnd(0,1,1,10000);

[h,p,stats]=chi2gof(x10,'cdf',{@normcdf,0,1})
[h,p,stats]=chi2gof(x100,'cdf',{@normcdf,0,1})
[h,p,stats]=chi2gof(x1000,'cdf',{@normcdf,0,1})
[h,p,stats]=chi2gof(x10000,'cdf',{@normcdf,0,1})