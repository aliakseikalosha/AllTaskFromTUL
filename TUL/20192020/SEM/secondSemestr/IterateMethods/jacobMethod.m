function [x,count] = jacobMethod(a,b, eps)
    x=zeros(size(b));
    [m,n] = size(a);
    count = 0;
    dInv= inv(a.*(eye(m)));
    l=a;
    u=a;
    for i=1:n
        for j = i:n
            l(i,j) = 0;
            u(n-i,j) = 0;
        end
    end
    l
    u
    dInv
    
end

