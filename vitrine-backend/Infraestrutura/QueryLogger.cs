using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using vitrine_backend.Background;
using vitrine_backend.Trace;

namespace vitrine_backend.Infraestrutura
{
    public class QueryLogger : DiagnosticEventListener
    {
        private static Stopwatch queryTimer;
        private readonly IBackgroundQueue<Log> queue;
        public QueryLogger(IBackgroundQueue<Log> queue)
        {
            this.queue = queue;
        }

        public override IActivityScope ExecuteRequest(IRequestContext context)
        {
            return new RequestScope(queue, context);
        }

        private class RequestScope : IActivityScope
        {
            private readonly IRequestContext context;
            private readonly IBackgroundQueue<Log> queue;

            public RequestScope(IBackgroundQueue<Log> queue, IRequestContext context)
            {
                this.queue = queue;
                this.context = context;

                queryTimer = new Stopwatch();
                queryTimer.Start();
            }

            public void Dispose()
            {
                if (context.Document != null)
                {
                    var st = new StringBuilder(context.Document.ToString(true));

                    st.AppendLine();

                    if (context.Variables != null)
                    {
                        var variables = context.Variables!.ToList();
                        if (variables.Count > 0)
                            Build(st);
                    }

                    queryTimer.Stop();

                    st.AppendFormat($"Ellapsed time {queryTimer.Elapsed.TotalMilliseconds:0.#} milliseconds.");

                    queue.Enqueue(new Log(st.ToString()));
                }

                void Build(StringBuilder st)
                {
                    st.AppendFormat($"Variables {Environment.NewLine}");

                    try
                    {
                        foreach (var value in context.Variables!)
                        {
                            static string PadRight(string existStr, int lengthToPadTo)
                            {
                                if (string.IsNullOrEmpty(existStr))
                                    return "".PadRight(lengthToPadTo);

                                if (existStr.Length > lengthToPadTo)
                                    return existStr.Substring(0, lengthToPadTo);

                                return existStr + " ".PadRight(lengthToPadTo - existStr.Length);
                            }

                            st.AppendFormat($"  {PadRight(value.Name, 20)} :  {PadRight(value.Value.ToString(), 20)}: {value.Type}");
                            st.AppendFormat($"{Environment.NewLine}");
                        }
                    }
                    catch
                    {
                        st.Append("  formatting variables error. continuing...");
                        st.AppendFormat($"{Environment.NewLine}");
                    }
                }
            }
        }
    }
}
