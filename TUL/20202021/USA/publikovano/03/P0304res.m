clear all
clc

x=0:0.1:20;
y=1-exp(-x./3);
plot(x,y)
title('Graf distribuèní funkce');
xlabel('cas [hod]');
ylabel('distribuèní funkce F(t)');