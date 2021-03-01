x=normrnd(20,10,1,50);
y=normrnd(30,10,1,50);
z=[x,y]

[h,p,stats]=chi2gof(z,'cdf',{@normcdf,mean(z),std(z)})

