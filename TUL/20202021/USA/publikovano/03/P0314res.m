clear all
clc

syms a t;
a=[0.1,0.2,0.5];

F=1-exp(-a*t);
f=diff(F)
lambda=f./(1-F)
strhod=int(t.*f,0,inf)
rozptyl=int((t-strhod).^2.*f,0,inf)
smerodch=sqrt(rozptyl)

tretimoment=int((t-strhod).^3.*f,0,inf);
ctvrtymoment=int((t-strhod).^4.*f,0,inf);

sikmost=tretimoment./smerodch.^3
spicatost=ctvrtymoment./smerodch.^4