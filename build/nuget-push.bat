FOR /r %%X IN (*.nupkg) DO (
    nuget push %%X 66ae3607-965b-401d-8d7d-a053f8484924 
) 
@pause