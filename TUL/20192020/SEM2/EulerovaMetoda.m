clc;
clear all;
close all;
m=80;
k=0.5*0.5*1.29*0.5^2*pi;
v(1)=50;
t(1)=0;
h = 0.5;
n = 60/h;
yp(1)=70;
f = @(t,v) -k*v^2/m;
for i=1:n
    t(i+1) = t(i)+h;
    v(i+1) = v(i)+h*f(t(i),v(i));
end
plot(t,v);
xlabel('t')
ylabel('v(t)')
hold on;
syms v
te=zeros(1,length(t));
ve=zeros(1,length(v));
ve(1)=50;
for i=1:n-1
    te(i+1) = te(i)+h;
     eqn = ve(i)+h*(-k*v^2/m)-v == 0;
     s = vpa(solve(eqn,v,'Real',true));
     if s(1) > 0
        ve(i+1)=subs(s(1));
     else
        ve(i+1)=subs(s(2));
     end
end
plot(te(1:end-1),ve);