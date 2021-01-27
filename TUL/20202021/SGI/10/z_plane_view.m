clear all;
close all;
clc;

%[N, Wn] = buttord([0.1 0.5], [0.2 0.4],3,40);
%[b,a] = butter(N,Wn, 'stop');

b=[1 0 1];		%Coefficients bk of the transfer function
a=[1];                      %Coefficients ak of the transfer function
limits=[-2,2];

astr='('; astr2='(';
for lp=1:length(a)
   if(a(lp)>=0),sgn='+';else sgn='-';end
   astr=[astr sgn num2str(abs(a(lp))) '*(exp(j*t))^(' num2str(-(lp-1)) ')'];
   astr2=[astr2 sgn num2str(abs(a(lp))) '*(x+i*y)^(' num2str(-(lp-1)) ')'];
end   
astr=[astr ')']; astr2=[astr2 ')'];

bstr='('; bstr2='(';
for lp=1:length(b)
   if(b(lp)>=0),sgn='+';else sgn='-';end
   bstr=[bstr sgn num2str(abs(b(lp))) '*(exp(j*t))^(' num2str(-(lp-1)) ')'];
   bstr2=[bstr2 sgn num2str(abs(b(lp))) '*(x+i*y)^(' num2str(-(lp-1)) ')'];
end   
bstr=[bstr ')'];
bstr2=[bstr2 ')'];

Hz=['abs((' bstr ')/(' astr '))'];
Hz2=['abs((' bstr2 ')/(' astr2 '))'];

h=ezplot3('cos(t)','sin(t)',Hz);
set(h,'LineWidth',5);set(h,'Color','red');
hold on
ezsurf(Hz2,limits,50)
title('Filter');
xlabel('Re(z)');
ylabel('Im(z)');
zlabel('abs(H(z))');

figure;
[H,W]=freqz(b,a,1024);
subplot(1,2,1);
plot(W/2,abs(H));
title('Magnitude Response');
xlabel('ω [rad]');
ylabel('|H(z)|');
subplot(1,2,2);
plot(W/2,atan2(imag(H),real(H)));
title('Phase Response');
xlabel('ω [rad]');
ylabel('φ(z) [rad]');

figure
zplane(b,a)
title('Z-plane');
