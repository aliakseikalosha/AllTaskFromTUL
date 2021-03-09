x=importdata('P0914.mat');

wblplot(x)


param=wblfit(x)
[h,p,stats]=chi2gof(x,'cdf',{@wblcdf,param(1),param(2)})
