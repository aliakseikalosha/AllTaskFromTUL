function [x] = GEM(A,b)
 x = zeros(1, size(b,2));
 for i=1:size(A,2)
     for j=i+1:size(A,1)
        if  A(i,i) == 0
            for k=1:size(A,1)
                if(A(k,i)~= 0)
                    t=A(k,:);
                    A(k,:)=A(i,:);
                    A(i,:)=t;
                    t=b(k);
                    b(k)=b(i);
                    b(i)=t;
                end
            end
        end
        m=(A(j,i)/A(i,i));
        A(j,:) = A(j,:) - m.*A(i,:);
        b(j) = b(j) - m.*b(i);
     end
 end
 
 for i=size(A,2):-1:1
    for j=i-1:-1:1
        if  A(i,i) == 0
            for k=1:size(A,1)
                if(A(k,i)~= 0)
                    t=A(k,:);
                    A(k,:)=A(i,:);
                    A(i,:)=t;
                    t=b(k);
                    b(k)=b(i);
                    b(i)=t;
                end
            end
        end
        m=(A(j,i)/A(i,i));
        A(j,:) = A(j,:) - m.*A(i,:);
        b(j) = b(j) - m.*b(i);
    end
 end
 for i=1:size(A,2)
    x(i) = b(i)/A(i,i);
 end
end

