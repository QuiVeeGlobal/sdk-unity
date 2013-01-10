using System;

public class MockRoar : DefaultRoar, IRoar
{
  public RequestSender api;

  new public void Awake ()
  {
    config = new Roar.implementation.Config ();
    Logger logger = new Logger ();

    api = new MockRequestSender (config, this, logger);
    webAPI = new global::WebAPI(api);
    Roar.implementation.DataStore data_store = new Roar.implementation.DataStore (webAPI, logger);
    user = new Roar.implementation.Components.User (webAPI.user, data_store, logger);
    properties = new Roar.implementation.Components.Properties (data_store);
    inventory = new Roar.implementation.Components.Inventory (webAPI.items, data_store, logger);
    shop = new Roar.implementation.Components.Shop (webAPI.shop, data_store, logger);
    friends = new Roar.implementation.Components.Friends (webAPI.friends, data_store, logger);
    tasks = new Roar.implementation.Components.Tasks (webAPI.tasks, data_store);

    urbanAirship = new Roar.implementation.Adapters.UrbanAirship (webAPI);

    // Apply public settings
    // TODO: Not sure what this should be now.
    // Config.game = gameKey;
  }
}

