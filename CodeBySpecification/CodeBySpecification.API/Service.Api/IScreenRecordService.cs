namespace CodeBySpecification.API
{
	public interface IScreenRecordService
	{
		string OutputFile { get; set; }

		void Start();

		void Stop();
	}
}