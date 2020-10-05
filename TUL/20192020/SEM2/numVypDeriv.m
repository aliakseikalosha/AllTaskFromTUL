syms x
f = sin(x);
x0 = 1;
h = [0.8,0.4,0.2,0.1];
d1 = vpa((subs(f, x0+h)-subs(f, x0))./h , 5)
d2 = vpa((subs(f,x0+h)-subs(f, x0-h))./(2*h),5)
d3 = vpa((4/3)*d2 - (1/3)*(subs(f, x0+2*h)-subs(f,x0-2*h))./(4*h),5)
plot(h, d1, 'r')
hold on;
plot(h, d2, 'b')
plot(h, d3, 'g')
hold off;
chyba1 = -(0.5).*h.*subs(diff(f, 2), x0);
chyba11 = -(0.5).*h.*subs(diff(f, 2), x0+h);
chyba2 = -((h.^2)/6).*(subs(diff(f, 3), x0) + subs(diff(f, 3), x0-h));
chyba21 = -((h.^2)/6).*(subs(diff(f, 3), x0+h) + subs(diff(f, 3), x0));
chyba3 = ((h.^4)/120).*(subs(diff(f, 5), x0) + subs(diff(f, 5), x0-h));
chyba31 = ((h.^4)/120).*(subs(diff(f, 5), x0-h) + subs(diff(f, 5), x0));
figure;
plot(h, (chyba1+chyba11)/2, 'r')
hold on;
plot(h, (chyba2+chyba21)/2, 'b')
plot(h, (chyba3+chyba31)/2, 'g')