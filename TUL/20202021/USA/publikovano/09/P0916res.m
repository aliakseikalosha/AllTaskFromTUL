t=[37,48,54,75,81,104,123,141,156,187,195,213,241,254,271,289,312,345,395,412,461,512,651,731];

wblplot(t)

a(1:length(t),1)=t';
a(:,2)=expcdf(t,mean(t));

[h1,p1,ksstat1,cv]=kstest(t,a)

param=wblfit(t);
a(:,2)=expcdf(t,param(1),param(2));
[h2,p2,ksstat2,cv]=kstest(t,a)