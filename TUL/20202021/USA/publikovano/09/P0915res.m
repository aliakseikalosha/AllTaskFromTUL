vyska=[162,167,170,171,172,175,178,179,180,181,182,184,185,187,191,195]; 

normplot(vyska)

a(1:length(vyska),1)=vyska';
a(1:length(vyska),2)=normcdf(vyska',mean(vyska),std(vyska));
[h,p,ksstat,cv]=kstest(vyska,a)

[h,p,kstat,critval]=lillietest(vyska,0.05,'norm')
