% pravd�podobnost 3 nebo 4 z�sah�
p3=nchoosek(4,4)*0.1^4+nchoosek(4,3)*0.1^3*0.9;
%pravd�podobnost 2 zasah�
p2=nchoosek(4,2)*0.9^2*0.1^2;
%pravd�podobnost 1 z�sahu
p1=nchoosek(4,1)*0.9^3*0.1;
%pravd�podobnost 0 z�sah�
p0=nchoosek(4,0)*0.9^4;
p=p0+p1+p2+p3;

vysledek=p3*0.9+p2*0.4+p1*0.1