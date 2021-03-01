x=importdata('P0909.mat');

normplot(x)

str_hod=mean(x)
sm_odch=std(x)
[h,p,stats]=chi2gof(x,'cdf',{@normcdf,mean(x),std(x)})
[h,p,stats]=chi2gof(x,'cdf',{@normcdf,15,5})
