x=poissrnd(10,1,100);

[h1,p1,stats1]=chi2gof(x,'cdf',{@poisscdf,mean(x)})

pridane=[14,15,17,18,19,21,22,24,26,27,27,28,32,34,36];
x=[x,pridane];

[h2,p2,stats2]=chi2gof(x,'cdf',{@poisscdf,mean(x)})
