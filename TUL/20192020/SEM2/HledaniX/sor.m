function [poc] = sor(A,ep)
b=ceil(randn(size(A,1),1));
x=zeros(size(b, 1), 1);
poc=0;
D=inv(diag(eig(A)));
E=eye(size(D,1), size(A,2));
G=E-D*A;
w=2/(1+sqrt(1-(max(abs(eig(G))))^2));
for i=2:length(A)
    sum1=0;
    sum2=0;
    for j=1:i-1
        sum1=sum1+A(i,j)*x(i);
    end
    for d=i+1:length(A)
        sum2=sum2+A(i,d)*x(d);
    end
    x(i)=(1-w)*x(i-1)+w/A(i,i)*(b(i)-sum1-sum2);
    if ~(abs(x(i)-x(i-1))<=ep*abs(x(i-1)))
        poc=poc+1;
    end  
end
disp("x = " + x)
end