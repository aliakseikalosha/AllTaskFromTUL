x=normrnd(170,50,1,100);

subplot(2,1,1);
plot=normplot(x);
subplot(2,1,2);
plot=boxplot(x);

%ad a)
[h,p,ci]=ttest(x)

%ad b)

q25=quantile(x,0.25);
q75=quantile(x,0.75);

median_dol=median(x)-1.57*(q75-q25)/sqrt(length(x))
median_hor=median(x)+1.57*(q75-q25)/sqrt(length(x))