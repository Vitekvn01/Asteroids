using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AppodealAds.Unity.Common;
using UnityEngine;

namespace AppodealAds.Unity.Dummy
{
    using Appodeal = Api.Appodeal;

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class DummyClient : IAppodealAdsClient
    {
        public void initialize(string appKey, int adTypes, IAppodealInitializationListener listener)
        {
            Debug.Log("Call to Appodeal.initialize on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public bool isInitialized(int adType)
        {
            Debug.Log("Call Appodeal.isInitialized on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public bool show(int adTypes)
        {
            Debug.Log("Call to Appodeal.show on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public bool show(int adTypes, string placement)
        {
            Debug.Log("Call to Appodeal.show on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public bool showBannerView(int YAxis, int XAxis, string Placement)
        {
            Debug.Log("Call to Appodeal.showBannerView on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public bool showMrecView(int YAxis, int XGravity, string Placement)
        {
            Debug.Log("Call to Appodeal.showMrecView on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public bool isLoaded(int adTypes)
        {
            Debug.Log("Call to Appodeal.showBannerView on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public void cache(int adTypes)
        {
            Debug.Log("Call to Appodeal.cache on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void hide(int adTypes)
        {
            Debug.Log("Call to Appodeal.hide on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void hideBannerView()
        {
            Debug.Log("Call to Appodeal.hideBannerView on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void hideMrecView()
        {
            Debug.Log("Call to Appodeal.hideMrecView on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public bool isPrecache(int adTypes)
        {
            Debug.Log("Call to Appodeal.isPrecache on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public void setAutoCache(int adTypes, bool autoCache)
        {
            Debug.Log("Call to Appodeal.setAutoCache on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setSmartBanners(bool value)
        {
            Debug.Log("Call to Appodeal.setSmartBanners on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public bool isSmartBannersEnabled()
        {
             Debug.Log("Call to Appodeal.isSmartBannersEnabled on not supported platform. To test advertising, install your application on the Android/iOS device.");
             return false;
        }

        public void setBannerAnimation(bool value)
        {
            Debug.Log("Call to Appodeal.setBannerAnimation on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setTabletBanners(bool value)
        {
            Debug.Log("Call to Appodeal.setTabletBanners on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setBannerRotation(int leftBannerRotation, int rightBannerRotation)
        {
            Debug.Log("Call to Appodeal.setBannerRotation on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setTesting(bool test)
        {
            Debug.Log("Call to Appodeal.setTesting on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setLogLevel(Appodeal.LogLevel logging)
        {
            Debug.Log("Call to Appodeal.setLogLevel on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setChildDirectedTreatment(bool value)
        {
            Debug.Log("Call to Appodeal.setChildDirectedTreatment on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void disableNetwork(string network)
        {
            Debug.Log("Call to Appodeal.disableNetwork on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void disableNetwork(string network, int adTypes)
        {
            Debug.Log("Call to Appodeal.disableNetwork on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void disableLocationPermissionCheck()
        {
            Debug.Log("Call to Appodeal.disableLocationPermissionCheck on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setTriggerOnLoadedOnPrecache(int adTypes, bool onLoadedTriggerBoth)
        {
            Debug.Log("Call to Appodeal.setTriggerOnLoadedOnPrecache on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void muteVideosIfCallsMuted(bool value)
        {
            Debug.Log("Call to Appodeal.muteVideosIfCallsMuted on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void showTestScreen()
        {
            Debug.Log("Call to Appodeal.showTestScreen on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public string getVersion()
        {
            return Appodeal.APPODEAL_PLUGIN_VERSION;
        }

        public long getSegmentId()
        {
            Debug.Log("Call to Appodeal.getSegmentId on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return -1;
        }

        public bool canShow(int adTypes)
        {
            Debug.Log("Call to Appodeal.canShow on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public bool canShow(int adTypes, string placement)
        {
            Debug.Log("Call to Appodeal.canShow on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public void setCustomFilter(string name, bool value)
        {
            Debug.Log("Call to Appodeal.setCustomFilter on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setCustomFilter(string name, int value)
        {
            Debug.Log("Call to Appodeal.setCustomFilter on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setCustomFilter(string name, double value)
        {
            Debug.Log("Call to Appodeal.setCustomFilter on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setCustomFilter(string name, string value)
        {
            Debug.Log("Call to Appodeal.setCustomFilter on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void resetCustomFilter(string name)
        {
            Debug.Log("Call to Appodeal.resetCustomFilter on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void trackInAppPurchase(double amount, string currency)
        {
            Debug.Log("Call to Appodeal.trackInAppPurchase on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public List<string> getNetworks(int adTypes)
        {
            Debug.Log("Call to Appodeal.getNetworks on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return null;;
        }

        public string getRewardCurrency(string placement)
        {
            Debug.Log("Call to Appodeal.getRewardCurrency on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return "USD";
        }

        public double getRewardAmount(string placement)
        {
            Debug.Log("Call to Appodeal.getRewardAmount on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return 0;
        }

        public string getRewardCurrency()
        {
            Debug.Log("Call to Appodeal.getRewardCurrency on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return "USD";
        }

        public double getRewardAmount()
        {
            Debug.Log("Call to Appodeal.getRewardAmount on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return 0;
        }

        public double getPredictedEcpm(int adType)
        {
            Debug.Log("Call to Appodeal.getPredictedEcpm on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return 0;
        }

        public double getPredictedEcpmForPlacement(int adType, string placement)
        {
            Debug.Log("Call to Appodeal.getPredictedEcpmForPlacement on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return 0;
        }

        public void setExtraData(string key, bool value)
        {
            Debug.Log("Call to Appodeal.setExtraData(string key, bool value) on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setExtraData(string key, int value)
        {
            Debug.Log("Call to Appodeal.setExtraData(string key, int value) on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setExtraData(string key, double value)
        {
            Debug.Log("Call to Appodeal.setExtraDatastring key, double value) on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setExtraData(string key, string value)
        {
            Debug.Log("Call to Appodeal.setExtraData(string key, string value) on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void resetExtraData(string key)
        {
            Debug.Log("Call to Appodeal.resetExtraData(string key) on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setInterstitialCallbacks(IInterstitialAdListener listener)
        {
            Debug.Log("Call to Appodeal.setInterstitialCallbacks on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setRewardedVideoCallbacks(IRewardedVideoAdListener listener)
        {
            Debug.Log("Call to Appodeal.setRewardedVideoCallbacks on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setBannerCallbacks(IBannerAdListener listener)
        {
            Debug.Log("Call to Appodeal.setBannerCallbacks on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setMrecCallbacks(IMrecAdListener listener)
        {
            Debug.Log("Call to Appodeal.setMrecCallbacks on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setAdRevenueCallback(IAdRevenueListener listener)
        {
            Debug.Log("Call to Appodeal.setAdRevenueCallback on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void destroy(int adTypes)
        {
            Debug.Log("Call to Appodeal.destroy on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setSharedAdsInstanceAcrossActivities(bool value)
        {
            Debug.Log("Call to Appodeal.setSharedAdsInstanceAcrossActivities on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void setUseSafeArea(bool value)
        {
            Debug.Log("Call to Appodeal.setUseSafeArea on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public bool isAutoCacheEnabled(int adType)
        {
            Debug.Log("Call to Appodeal.isAutoCacheEnabled on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return false;
        }

        public void setUserId(string id)
        {
            Debug.Log("Call to Appodeal.setUserId on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public string getUserId()
        {
            Debug.Log("Call to Appodeal.getUserId on not supported platform. To test advertising, install your application on the Android/iOS device.");
            return string.Empty;
        }

        public void logEvent(string eventName, Dictionary<string, object> eventParams)
        {
            Debug.Log("Call to Appodeal.logEvent on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void validatePlayStoreInAppPurchase(IPlayStoreInAppPurchase purchase, IInAppPurchaseValidationListener listener)
        {
            Debug.Log("Call to Appodeal.validatePlayStoreInAppPurchase on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }

        public void validateAppStoreInAppPurchase(IAppStoreInAppPurchase purchase, IInAppPurchaseValidationListener listener)
        {
            Debug.Log("Call to Appodeal.validateAppStoreInAppPurchase on not supported platform. To test advertising, install your application on the Android/iOS device.");
        }
    }
}
