clc;
clear all;
close all;
h=[0.5,0.1,0.01];
data=zeros(1,length(h)*3+1);
for i = 1:length(h)
    data(i) = obdform(h(i),1,2);
    data(i+length(h)) = lichobform(h(i),1,2);
    data(i+length(h)*2) = simpform(h(i),1,2);
end
data(end) = exp(2) - exp(1);
bar(data)

function obdel = obdform(h, a, b)
    f = @(x) exp(x);
    x = rozsah(h,a,b);
    obdel = 0;
    for i=1:length(x)-1
        a = x(i);
        b = x(i+1);
        obdel = obdel + (b-a)*f((a+b)/2);
    end
end

function lich = lichobform(h, a, b)
    f = @(x) exp(x);
    x = rozsah(h,a,b);
    lich = 0;
    for i=1:length(x)-1
    lich = lich + (x(i+1)-x(i))/2*(f(x(i))+f(x(i+1)));
    end
end
function simp = simpform(h, a, b)
    f = @(x) exp(x);
    x = rozsah(h,a,b);
    simp=0;
    for i=1:length(x)-1
        a = x(i);
        b = x(i+1);
        simp = simp + (b-a)/6*(f(a)+4*f((a+b)/2)+f(b));
    end
end

function x=rozsah(h,a,b)
    x=a:h:b;
end