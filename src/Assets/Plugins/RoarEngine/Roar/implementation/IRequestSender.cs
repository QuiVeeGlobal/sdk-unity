using System.Collections;

public interface IRequestCallback
{
	void OnRequest (Roar.RequestResult info);
};

// Provides a little more structure to the IRequestCallback function.
public abstract class SimpleRequestCallback : IRequestCallback
{
	protected Roar.RequestCallback cb;

	public SimpleRequestCallback (Roar.RequestCallback in_cb)
	{
		cb = in_cb;
	}

	public virtual void OnRequest ( Roar.RequestResult info)
	{
		Prologue ();
		
		if (info.code != 200)
		{
			OnFailure (info);
		} else
		{
			OnSuccess (info);
		}
		
		if (cb != null)
		{
			cb (info);
		}
		
		Epilogue ();
	}
  
	public abstract void OnSuccess (Roar.RequestResult info);

	public virtual void OnFailure (Roar.RequestResult info)
	{}

	public virtual void Prologue ()
	{}

	public virtual void Epilogue ()
	{}
};

public interface IRequestSender
{
	void MakeCall (string apicall, Hashtable args, IRequestCallback cb);
}

