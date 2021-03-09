x=importdata('P0910.mat');

normplot(x)

str_hod=mean(x)
sm_odch=std(x)
[h,p,stats]=chi2gof(x,'cdf',{@normcdf,20,10})

boxplot(x)
%odstraníme 3 nejnižší a 5 nejvyššího hodnot, jsou to odlehlé hodnoty

x=sort(x);
x(1:3)=NaN;
x(106:110)=NaN;
%boxplot(x) by nalezlo další odlehlou hodnotu
[h,p,stats]=chi2gof(x,'cdf',{@normcdf,20,10});