function [poc] = GausSeid(A,ep)
b=ceil(randn(size(A,1),1));
x=zeros(size(b, 1), 1);
poc=0;
for i=2:length(A)
    sum(1)=0;
    sum(2)=0;
    for j=1:i-1
        sum(1)=sum(1)+A(i,j)*x(i);
    end
    for d=i+1:length(A)
        sum(2)=sum(2)+A(i,d)*x(d);
    end
    x(i)=1/A(i,i)*(b(i)-sum(1)-sum(2));
    if ~(abs(x(i)-x(i-1))<=ep*abs(x(i-1)))
        poc=poc+1;
    end  
end
disp("x = " + x)
end