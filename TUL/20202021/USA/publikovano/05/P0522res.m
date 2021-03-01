x=-10:0.1:10;
y=normpdf(x,0,1)

figure
subplot (2,2,1)
y=normpdf(x,0,1)
plot(x,y)
title ('normalni rozdeleni mu=0, sigma=1')

subplot (2,2,2)
y=normpdf(x,0,2)
plot(x,y)
title ('normalni rozdeleni mu=0, sigma=2')

subplot (2,2,3)
y=normpdf(x,4,1)
plot(x,y)
title ('normalni rozdeleni mu=4, sigma=1')

subplot (2,2,4)
y=normpdf(x,4,2)
plot(x,y)
title ('normalni rozdeleni mu=4, sigma=2')