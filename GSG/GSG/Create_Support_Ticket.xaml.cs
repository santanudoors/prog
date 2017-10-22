using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Plugin.Settings;
using Plugin.Messaging;



namespace GSG
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create_Support_Ticket : ContentPage
    {
        public  Create_Support_Ticket()
        {
            InitializeComponent();

          

        }


        protected async override void OnAppearing()
        {
            await GetGeoLocation();
            base.OnAppearing();
        }

        private async Task GetGeoLocation()
        {

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 10;

            var geoCoder = new Geocoder();

            if (locator.IsGeolocationEnabled == true)
            {

                cmd_support_ticket.IsEnabled = true;

                if (locator.IsGeolocationAvailable == true)
                {
                    cmd_support_ticket.IsEnabled = true;
                    var position = await locator.GetPositionAsync(TimeSpan.FromMilliseconds(1000));

                    if (position != null)
                    {

                        Position myPinPosition = new Position(position.Latitude, position.Longitude);

                        string ReverseGeoAddress = "";
                        string OnlyAddress = "";
                        int AddressCount = 0;

                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(myPinPosition);
                        foreach (var address in possibleAddresses)
                        {
                            if (AddressCount == 0)
                            {
                                OnlyAddress = address;
                                AddressCount += 1;

                            }
                            ReverseGeoAddress += address + "\n";
                        }


                        txt_Current_Location.Text = "Your Current Location :" + OnlyAddress;

                        MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMeters(300)));
                        MyMap.Pins.Add(new Pin
                        {
                            Type = PinType.Place,
                            Label = ReverseGeoAddress,
                            Position = myPinPosition
                        }
                            );
                    }
                }
                else
                {
                    cmd_support_ticket.Text = "Call Support";
                    await DisplayAlert("Alert", "You location is not available", "Ok");
                }
            }
            else
            {
                cmd_support_ticket.Text = "Call Support";
                await DisplayAlert("Alert", "Enable your location service please", "Ok");
            }
        }

        private void cmd_support_ticket_Clicked(object sender, EventArgs e)
        {
            txt_issue.Text = "";
            if (cmd_support_ticket.Text == "Call Support")
            {
                var phoneDialer = CrossMessaging.Current.PhoneDialer;
               
                //CrossMessaging.Current.Settings().Phone.AutoDial = true;
                if (phoneDialer.CanMakePhoneCall)
                    phoneDialer.MakePhoneCall("+917064888888");
            }
            else
            { 
                var smsMessanger = CrossMessaging.Current.SmsMessenger;
                if (smsMessanger.CanSendSmsInBackground)
                {
                    smsMessanger.SendSmsInBackground("7064888888", "Support Needed at " + txt_Current_Location.Text + " issue reported " + txt_issue.Text);
                }
                DisplayAlert("Support Ticket", "Support ticket created successfully, we will contact you soon.", "Ok");
            }
        }
    }
}