using System.Text.Json.Serialization;

namespace GIHUN_MVC_Project.Models.Hotels
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Address
    {
        [JsonPropertyName("addressLine")]
        public string addressLine { get; set; }

        [JsonPropertyName("city")]
        public string city { get; set; }

        [JsonPropertyName("province")]
        public string province { get; set; }

        [JsonPropertyName("countryCode")]
        public string countryCode { get; set; }

        [JsonPropertyName("firstAddressLine")]
        public string firstAddressLine { get; set; }

        [JsonPropertyName("secondAddressLine")]
        public string secondAddressLine { get; set; }
    }

    public class Amenities
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("topAmenities")]
        public TopAmenities topAmenities { get; set; }

        [JsonPropertyName("propertyContentPreferences")]
        public object propertyContentPreferences { get; set; }

        [JsonPropertyName("amenitiesDialog")]
        public AmenitiesDialog amenitiesDialog { get; set; }

        [JsonPropertyName("takeover")]
        public Takeover takeover { get; set; }

        [JsonPropertyName("amenitiesAdaptExSuccessEvents")]
        public List<object> amenitiesAdaptExSuccessEvents { get; set; }
    }

    public class AmenitiesDialog
    {
        [JsonPropertyName("trigger")]
        public Trigger trigger { get; set; }

        [JsonPropertyName("toolbar")]
        public Toolbar toolbar { get; set; }
    }

    public class Analytics
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("linkName")]
        public string linkName { get; set; }

        [JsonPropertyName("referrerId")]
        public string referrerId { get; set; }

        [JsonPropertyName("uisPrimeMessages")]
        public List<UisPrimeMessage> uisPrimeMessages { get; set; }
    }

    public class Attributes
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("checkInDate")]
        public object checkInDate { get; set; }

        [JsonPropertyName("checkoutDate")]
        public object checkoutDate { get; set; }

        [JsonPropertyName("regionId")]
        public string regionId { get; set; }

        [JsonPropertyName("roomConfiguration")]
        public List<RoomConfiguration> roomConfiguration { get; set; }
    }

    public class Badge
    {
        [JsonPropertyName("accessibility")]
        public string accessibility { get; set; }

        [JsonPropertyName("size")]
        public string size { get; set; }

        [JsonPropertyName("text")]
        public string text { get; set; }

        [JsonPropertyName("theme")]
        public string theme { get; set; }
    }

    public class ClientSideAnalytics
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("linkName")]
        public string linkName { get; set; }

        [JsonPropertyName("referrerId")]
        public string referrerId { get; set; }

        [JsonPropertyName("eventType")]
        public string eventType { get; set; }
    }

    public class Coordinates
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("latitude")]
        public double? latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double? longitude { get; set; }
    }

    public class Data
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("summary")]
        public Summary summary { get; set; }

        [JsonPropertyName("telesales")]
        public object telesales { get; set; }

        [JsonPropertyName("reviewInfo")]
        public ReviewInfo reviewInfo { get; set; }

        [JsonPropertyName("propertyDetails")]
        public PropertyDetails propertyDetails { get; set; }

        [JsonPropertyName("propertyContentSectionGroups")]
        public PropertyContentSectionGroups propertyContentSectionGroups { get; set; }

        [JsonPropertyName("saveTripItem")]
        public SaveTripItem saveTripItem { get; set; }

        [JsonPropertyName("experimental")]
        public Experimental experimental { get; set; }
    }

    public class Dialog
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("icon")]
        public Icon icon { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("clientSideAnalytics")]
        public object clientSideAnalytics { get; set; }
    }

    public class Editorial
    {
        [JsonPropertyName("content")]
        public List<string> content { get; set; }
    }

    public class Experimental
    {
        [JsonPropertyName("pageLayoutExperimentalFeature")]
        public PageLayoutExperimentalFeature pageLayoutExperimentalFeature { get; set; }
    }

    public class Header
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("text")]
        public string text { get; set; }

        [JsonPropertyName("subText")]
        public object subText { get; set; }

        [JsonPropertyName("mark")]
        public object mark { get; set; }
    }

    public class Highlight
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("header")]
        public Header header { get; set; }

        [JsonPropertyName("icon")]
        public object icon { get; set; }

        [JsonPropertyName("impressionAnalytics")]
        public object impressionAnalytics { get; set; }

        [JsonPropertyName("jumpLink")]
        public object jumpLink { get; set; }

        [JsonPropertyName("items")]
        public List<Item> items { get; set; }

        [JsonPropertyName("adaptExAttemptEvents")]
        public object adaptExAttemptEvents { get; set; }

        [JsonPropertyName("seeMoreAction")]
        public object seeMoreAction { get; set; }

        [JsonPropertyName("adaptExSuccessEvents")]
        public object adaptExSuccessEvents { get; set; }
    }

    public class Icon
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("size")]
        public object size { get; set; }

        [JsonPropertyName("token")]
        public string token { get; set; }

        [JsonPropertyName("theme")]
        public object theme { get; set; }

        [JsonPropertyName("title")]
        public object title { get; set; }

        [JsonPropertyName("spotLight")]
        public object spotLight { get; set; }
    }

    public class ImpressionAnalytics
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("event")]
        public string @event { get; set; }

        [JsonPropertyName("referrerId")]
        public string referrerId { get; set; }

        [JsonPropertyName("linkName")]
        public string linkName { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("text")]
        public string text { get; set; }

        [JsonPropertyName("state")]
        public object state { get; set; }

        [JsonPropertyName("moreInfo")]
        public object moreInfo { get; set; }

        [JsonPropertyName("icon")]
        public Icon icon { get; set; }

        [JsonPropertyName("subItems")]
        public object subItems { get; set; }

        [JsonPropertyName("highlights")]
        public object highlights { get; set; }
    }

    public class Large
    {
        [JsonPropertyName("url")]
        public string url { get; set; }

        [JsonPropertyName("aspectRatio")]
        public object aspectRatio { get; set; }

        [JsonPropertyName("description")]
        public string description { get; set; }
    }

    public class Location
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("mapTrigger")]
        public MapTrigger mapTrigger { get; set; }

        [JsonPropertyName("address")]
        public Address address { get; set; }

        [JsonPropertyName("parentRegion")]
        public ParentRegion parentRegion { get; set; }

        [JsonPropertyName("coordinates")]
        public Coordinates coordinates { get; set; }

        [JsonPropertyName("multiCityRegion")]
        public MultiCityRegion multiCityRegion { get; set; }

        [JsonPropertyName("whatsAround")]
        public WhatsAround whatsAround { get; set; }

        [JsonPropertyName("mapDialog")]
        public MapDialog mapDialog { get; set; }

        [JsonPropertyName("staticImage")]
        public StaticImage staticImage { get; set; }

        [JsonPropertyName("video")]
        public object video { get; set; }
    }

    public class MapDialog
    {
        [JsonPropertyName("trigger")]
        public Trigger trigger { get; set; }

        [JsonPropertyName("toolbar")]
        public object toolbar { get; set; }
    }

    public class MapTrigger
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("large")]
        public Large large { get; set; }
    }

    public class MultiCityRegion
    {
        [JsonPropertyName("id")]
        public string id { get; set; }
    }

    public class NearbyPOI
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("header")]
        public Header header { get; set; }

        [JsonPropertyName("icon")]
        public Icon icon { get; set; }

        [JsonPropertyName("impressionAnalytics")]
        public object impressionAnalytics { get; set; }

        [JsonPropertyName("jumpLink")]
        public object jumpLink { get; set; }

        [JsonPropertyName("items")]
        public List<Item> items { get; set; }

        [JsonPropertyName("adaptExAttemptEvents")]
        public object adaptExAttemptEvents { get; set; }

        [JsonPropertyName("seeMoreAction")]
        public object seeMoreAction { get; set; }

        [JsonPropertyName("adaptExSuccessEvents")]
        public object adaptExSuccessEvents { get; set; }
    }

    public class OverallScoreWithDescriptionA11y
    {
        [JsonPropertyName("value")]
        public string value { get; set; }

        [JsonPropertyName("icon")]
        public object icon { get; set; }

        [JsonPropertyName("accessibilityLabel")]
        public string accessibilityLabel { get; set; }

        [JsonPropertyName("subtexts")]
        public object subtexts { get; set; }

        [JsonPropertyName("state")]
        public object state { get; set; }
    }

    public class Overview
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("vipMessaging")]
        public string vipMessaging { get; set; }

        [JsonPropertyName("inventoryType")]
        public object inventoryType { get; set; }

        [JsonPropertyName("accessibilityLabel")]
        public string accessibilityLabel { get; set; }

        [JsonPropertyName("label")]
        public object label { get; set; }

        [JsonPropertyName("propertyRating")]
        public PropertyRating propertyRating { get; set; }
    }

    public class PageLayoutExperimentalFeature
    {
        [JsonPropertyName("templateName")]
        public string templateName { get; set; }
    }

    public class ParentRegion
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
    }

    public class Phrase
    {
        [JsonPropertyName("phraseParts")]
        public List<PhrasePart> phraseParts { get; set; }
    }

    public class PhrasePart
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("accessibility")]
        public string accessibility { get; set; }

        [JsonPropertyName("text")]
        public string text { get; set; }
    }

    public class Property
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("header")]
        public Header header { get; set; }

        [JsonPropertyName("icon")]
        public Icon icon { get; set; }

        [JsonPropertyName("impressionAnalytics")]
        public object impressionAnalytics { get; set; }

        [JsonPropertyName("jumpLink")]
        public object jumpLink { get; set; }

        [JsonPropertyName("items")]
        public List<Item> items { get; set; }

        [JsonPropertyName("adaptExAttemptEvents")]
        public object adaptExAttemptEvents { get; set; }

        [JsonPropertyName("seeMoreAction")]
        public object seeMoreAction { get; set; }

        [JsonPropertyName("adaptExSuccessEvents")]
        public object adaptExSuccessEvents { get; set; }
    }

    public class PropertyContentSectionGroups
    {
        [JsonPropertyName("cleanliness")]
        public object cleanliness { get; set; }
    }

    public class PropertyDetails
    {
        [JsonPropertyName("reviewSummary")]
        public ReviewSummary reviewSummary { get; set; }
    }

    public class PropertyRating
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("rating")]
        public int? rating { get; set; }

        [JsonPropertyName("accessibility")]
        public string accessibility { get; set; }

        [JsonPropertyName("icon")]
        public Icon icon { get; set; }
    }

    public class PropertyReviewCountDetails
    {
        [JsonPropertyName("shortDescription")]
        public string shortDescription { get; set; }
    }

    public class Remove
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("accessibility")]
        public string accessibility { get; set; }

        [JsonPropertyName("analytics")]
        public Analytics analytics { get; set; }
    }

    public class ReviewInfo
    {
        [JsonPropertyName("summary")]
        public Summary summary { get; set; }
    }

    public class ReviewRating
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("badge")]
        public Badge badge { get; set; }

        [JsonPropertyName("phrases")]
        public List<Phrase> phrases { get; set; }
    }

    public class ReviewSummary
    {
        [JsonPropertyName("overallScoreWithDescriptionA11y")]
        public OverallScoreWithDescriptionA11y overallScoreWithDescriptionA11y { get; set; }

        [JsonPropertyName("reviewDisclaimerHeading")]
        public object reviewDisclaimerHeading { get; set; }

        [JsonPropertyName("reviewDisclaimerLabel")]
        public string reviewDisclaimerLabel { get; set; }

        [JsonPropertyName("reviewDisclaimerAccessibilityLabel")]
        public string reviewDisclaimerAccessibilityLabel { get; set; }

        [JsonPropertyName("reviewDisclaimerValues")]
        public object reviewDisclaimerValues { get; set; }

        [JsonPropertyName("strategy")]
        public object strategy { get; set; }
    }

    public class RoomConfiguration
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("childAges")]
        public List<object> childAges { get; set; }

        [JsonPropertyName("numberOfAdults")]
        public int? numberOfAdults { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("data")]
        public Data data { get; set; }

        [JsonPropertyName("status")]
        public bool? status { get; set; }

        [JsonPropertyName("message")]
        public string message { get; set; }
    }

    public class Save
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("accessibility")]
        public string accessibility { get; set; }

        [JsonPropertyName("analytics")]
        public Analytics analytics { get; set; }
    }

    public class SaveTripItem
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("attributes")]
        public Attributes attributes { get; set; }

        [JsonPropertyName("initialChecked")]
        public bool? initialChecked { get; set; }

        [JsonPropertyName("itemId")]
        public string itemId { get; set; }

        [JsonPropertyName("remove")]
        public Remove remove { get; set; }

        [JsonPropertyName("save")]
        public Save save { get; set; }

        [JsonPropertyName("source")]
        public string source { get; set; }

        [JsonPropertyName("subscriptionAttributes")]
        public object subscriptionAttributes { get; set; }

        [JsonPropertyName("viewType")]
        public object viewType { get; set; }
    }

    public class SeeAllAction
    {
        [JsonPropertyName("trigger")]
        public Trigger trigger { get; set; }
    }

    public class SeeMoreAction
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("dialog")]
        public Dialog dialog { get; set; }

        [JsonPropertyName("trigger")]
        public Trigger trigger { get; set; }
    }

    public class StarRatingIcon
    {
        [JsonPropertyName("token")]
        public string token { get; set; }
    }

    public class StaticImage
    {
        [JsonPropertyName("description")]
        public string description { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }

        [JsonPropertyName("aspectRatio")]
        public object aspectRatio { get; set; }
    }

    public class Summary
    {
        [JsonPropertyName("fees")]
        public object fees { get; set; }

        [JsonPropertyName("id")]
        public string id { get; set; }

        [JsonPropertyName("name")]
        public string name { get; set; }

        [JsonPropertyName("bannerMessageCard")]
        public object bannerMessageCard { get; set; }

        [JsonPropertyName("latinAlphabetName")]
        public string latinAlphabetName { get; set; }

        [JsonPropertyName("tagline")]
        public string tagline { get; set; }

        [JsonPropertyName("starRatingIcon")]
        public StarRatingIcon starRatingIcon { get; set; }

        [JsonPropertyName("overview")]
        public Overview overview { get; set; }

        [JsonPropertyName("featuredMessages")]
        public object featuredMessages { get; set; }

        [JsonPropertyName("spaceOverview")]
        public object spaceOverview { get; set; }

        [JsonPropertyName("amenities")]
        public Amenities amenities { get; set; }

        [JsonPropertyName("location")]
        public Location location { get; set; }

        [JsonPropertyName("nearbyPOIs")]
        public NearbyPOI nearbyPOIs { get; set; }

        [JsonPropertyName("lodgingChatbot")]
        public object lodgingChatbot { get; set; }

        [JsonPropertyName("overallScoreWithDescriptionA11y")]
        public OverallScoreWithDescriptionA11y overallScoreWithDescriptionA11y { get; set; }

        [JsonPropertyName("propertyReviewCountDetails")]
        public PropertyReviewCountDetails propertyReviewCountDetails { get; set; }

        [JsonPropertyName("highlightMessageCard")]
        public object highlightMessageCard { get; set; }

        [JsonPropertyName("seeAllAction")]
        public SeeAllAction seeAllAction { get; set; }

        [JsonPropertyName("reviewRating")]
        public ReviewRating reviewRating { get; set; }
    }

    public class Takeover
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("amenityClosures")]
        public object amenityClosures { get; set; }

        [JsonPropertyName("highlight")]
        public List<Highlight> highlight { get; set; }

        [JsonPropertyName("property")]
        public List<Property> property { get; set; }
    }

    public class Toolbar
    {
        [JsonPropertyName("title")]
        public string title { get; set; }
    }

    public class TopAmenities
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("header")]
        public Header header { get; set; }

        [JsonPropertyName("icon")]
        public object icon { get; set; }

        [JsonPropertyName("impressionAnalytics")]
        public object impressionAnalytics { get; set; }

        [JsonPropertyName("jumpLink")]
        public object jumpLink { get; set; }

        [JsonPropertyName("items")]
        public List<Item> items { get; set; }

        [JsonPropertyName("adaptExAttemptEvents")]
        public List<object> adaptExAttemptEvents { get; set; }

        [JsonPropertyName("seeMoreAction")]
        public object seeMoreAction { get; set; }

        [JsonPropertyName("adaptExSuccessEvents")]
        public object adaptExSuccessEvents { get; set; }
    }

    public class Trigger
    {
        [JsonPropertyName("value")]
        public string value { get; set; }

        [JsonPropertyName("icon")]
        public Icon icon { get; set; }

        [JsonPropertyName("clientSideAnalytics")]
        public ClientSideAnalytics clientSideAnalytics { get; set; }

        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("theme")]
        public object theme { get; set; }

        [JsonPropertyName("secondaryValue")]
        public object secondaryValue { get; set; }

        [JsonPropertyName("accessibilityLabel")]
        public string accessibilityLabel { get; set; }
    }

    public class UisPrimeMessage
    {
        [JsonPropertyName("messageContent")]
        public string messageContent { get; set; }

        [JsonPropertyName("schemaName")]
        public string schemaName { get; set; }
    }

    public class WhatsAround
    {
        [JsonPropertyName("__typename")]
        public string __typename { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("editorial")]
        public Editorial editorial { get; set; }

        [JsonPropertyName("nearbyPOIs")]
        public List<NearbyPOI> nearbyPOIs { get; set; }

        [JsonPropertyName("impressionAnalytics")]
        public ImpressionAnalytics impressionAnalytics { get; set; }
    }


}
