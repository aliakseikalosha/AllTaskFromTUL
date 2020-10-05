function [x] = GausovaEliminace(A, b)
A=[A b]
for i=1:size(A,1)-1
    hlPrv=max(abs(A(:,i)));
    if(hlPrv ~= A(i,i))
        [r s]=find(A(:,i)==hlPrv);
        C=zeros(r,s);
        pomV=A(size(C, 1),:);
        A(size(C, 1),:) = A(i,:);
        A(i,:)=pomV;
    end
    for d=i+1:size(A,1)
        mnoz=A(d,i)/A(i,i);
        A(d,:)=mnoz*A(i,:)-A(d,:);
        
    end
end
disp(A)
d=A(:,end);
C=A(1:end, 1:end-1);
x=C\d;
end