% pravdìpodobnost 3 nebo 4 zásahù
p3=nchoosek(4,4)*0.1^4+nchoosek(4,3)*0.1^3*0.9;
%pravdìpodobnost 2 zasahù
p2=nchoosek(4,2)*0.9^2*0.1^2;
%pravdìpodobnost 1 zásahu
p1=nchoosek(4,1)*0.9^3*0.1;
%pravdìpodobnost 0 zásahù
p0=nchoosek(4,0)*0.9^4;
p=p0+p1+p2+p3;

vysledek=p3*0.9+p2*0.4+p1*0.1