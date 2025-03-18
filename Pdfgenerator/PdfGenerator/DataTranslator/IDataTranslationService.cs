using System.Collections.Generic;

namespace PdfGenerator;

public interface IDataTranslationService<TInput, TOutput>
{
    List<TOutput> TranslateData(List<TInput> data);
}
