x=[37,61,98,135,162,194,222,235,256,287,317,345,400,412,484,495,510,528,612,711,787,843,911,987,1014,1218,1512];

subplot(2,2,1);
plot=wblplot(x);
subplot(2,2,2);
plot=normplot(x);
subplot(2,2,3);
plot=boxplot(x);

q25=quantile(x,0.25);
q75=quantile(x,0.75);

median_dol=median(x)-1.57*(q75-q25)/sqrt(length(x))
median_hor=median(x)+1.57*(q75-q25)/sqrt(length(x))