using WebObjects = Roar.WebObjects;


class CallbackBridge<T> : IRequestCallback where T : WebObjects.IResponse, new()
{
	public ZWebAPI.Callback<T> cb_;
	public CallbackBridge( ZWebAPI.Callback<T> cb )
	{
		cb_ = cb;
	}
	public void OnRequest( Roar.RequestResult info )
	{
		Prologue ();
		if (info.code != IWebAPI.OK) {
			if (cb_ != null)
				cb_.OnError( info );
			OnFailure (info);
		} else {
			T result_data = new T();
			result_data.ParseXml( info.data );
			Roar.CallbackInfo<T> result = new Roar.CallbackInfo<T> (result_data, info.code, info.msg);
			if (cb_ != null)
				cb_.OnSuccess( result );
			OnSuccess( result );
		}
		Epilogue ();
	}
	
	public virtual void OnSuccess( Roar.CallbackInfo<T> info )
	{
	}
	
	public virtual void OnFailure (Roar.RequestResult info)
	{}
	
	public virtual void Prologue ()
	{}
	public virtual void Epilogue ()
	{}
}