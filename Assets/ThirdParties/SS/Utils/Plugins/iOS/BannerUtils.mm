//
//  ScreenUtils.mm
//  Unity-iPhone
//
//  Created by Anh Pham on 2018/08/21.
//
//
extern "C"
{
    float getWidth()
    {
        CGRect screenBounds = [[UIScreen mainScreen] bounds];
        return screenBounds.size.width;
    }
    
    float getHeight()
    {
        CGRect screenBounds = [[UIScreen mainScreen] bounds];
        return screenBounds.size.height;
    }
    
    float getScale()
    {
        return [[UIScreen mainScreen] scale];
    }
    
    float getSmartBannerHeight()
    {
        if (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad)
        {
            return 90;
        }
        
        return 50;
    }
    
    float getSmartBannerHeightRatio()
    {
        return (getSmartBannerHeight() / getHeight());
    }
}
