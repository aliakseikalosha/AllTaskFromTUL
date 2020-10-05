syms x
f = x^3 + x - 1;
x0=0.5;
while abs((subs(f, x0))) > 10^-5
    t=x0-subs(f, x0)/subs(diff(f),x0);
    x0=t;
end
disp(vpa(x0, 4))