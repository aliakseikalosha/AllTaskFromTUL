mu=12.01;
sigma=0.04;
n=80;

%pøípad ad a
xmin=mu-(sigma*norminv(0.975,0,1))/sqrt(n)
xmax=mu+(sigma*norminv(0.975,0,1))/sqrt(n)

%pøípad ad b
xmin=mu-(sigma*tinv(0.975,79))/sqrt(n)
xmax=mu+(sigma*tinv(0.975,79))/sqrt(n)

%rozdíl výsledkù
rozdil=-(sigma*tinv(0.975,79))/sqrt(n)+(sigma*norminv(0.975,0,1))/sqrt(n)