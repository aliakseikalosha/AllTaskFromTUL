clear all
clc

t=0:0.01:5;

hold on
plot(t,1-exp(-t.^0.8))
plot(t,1-exp(-t.^1))
plot(t,1-exp(-t.^1.5))
plot(t,1-exp(-t.^2))
plot(t,1-exp(-t.^2.5))
plot(t,1-exp(-t.^3))
plot(t,1-exp(-t.^10))

hold off