using dogcat.Models.Domain;

namespace dogcat.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> CommentGetAllAsync();

        //작성페이지 이름 종류 일련번호
        Task<List<Comment?>> CommentGetAsync(long Writeid);

        // 새로운 댓글(Cmment)생성하기
        Task<Comment> CommnetAddAsync(Comment comment);   // <- 작성자, 제목, 내용

        // 특정 댓글(Comment) 수정하기
        Task<Comment?> CommentUpdateAsync(Comment  comment);   // <- Id, 제목, 내용

        // 특정 Id 의 글(Write) 삭제하기
        Task<Comment?> CommentDeleteAsync(long id);
    }
}
