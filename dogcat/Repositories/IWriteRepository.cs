using dogcat.Models.Domain;

namespace dogcat.Repositories
{
 
    public interface IWriteRepository
    {
        //작성 페이지 모두 읽어오기
        Task<IEnumerable<Write>> GetAllAsync();

        //작성페이지 이름 종류 일련번호
        Task<Write?> GetAsync(long id);

        // 새로운 글 (Write) 생성하기
        Task<Write> AddAsync(Write write);   // <- 작성자, 제목, 내용

        // 특정 글(Write) 수정하기
        Task<Write?> UpdateAsync(Write write);   // <- Id, 제목, 내용

        // 특정 Id 의 글(Write) 삭제하기
        Task<Write?> DeleteAsync(long id);

        // 특정 Id 의 글 조회수 +1 증가
        Task<Write?> IncViewCntAsync(long id);

        Task<long> CountAsync(); //전체 글 개수
        Task<IEnumerable<Write>> GetFromRowAsync(int formRow, int pageRows); //페이지의 목록 읽어오기

    } // end repository
}
